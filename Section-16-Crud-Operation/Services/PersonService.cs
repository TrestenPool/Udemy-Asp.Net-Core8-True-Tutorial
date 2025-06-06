using System.ComponentModel.DataAnnotations;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Entities;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ServiceContracts;
using ServiceContracts.Enums;

namespace Services;

public class PersonService : IPersonService
{
  // Properties
  private PersonsDbContext _db;
  ICountriesService _countriesService;

  // Constructor
  public PersonService(PersonsDbContext personsDbContext, ICountriesService countriesService)
  {
    _db = personsDbContext;
    _countriesService = countriesService;
  }

  // Public Methods
  public async Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest)
  {
    // personaddrequest is null
    ArgumentNullException.ThrowIfNull(personAddRequest, nameof(personAddRequest));

    // Validation the properties on the arg personAddRequest
    ValidationHelper.ModelValidation(personAddRequest);

    // convert the personAddRequest to person
    Person person = personAddRequest.ToPerson();

    // generate new guid for the person
    person.PersonId = Guid.NewGuid();

    // add the person to the list
    _db.Persons.Add(person);
    await _db.SaveChangesAsync();

    // await _db.sp_InsertPerson(person);

    // return the person response
    return person.ToPersonResponse();
  }

  public async Task<List<PersonResponse>> GetAllPersons()
  {
    var persons = await _db.Persons.Include("Country").ToListAsync();
    return persons.Select(p => p.ToPersonResponse()).ToList();
  }

  public async Task<PersonResponse?> GetPersonByPersonId(Guid? personId)
  {
    // throw null exc if arg is null
    ArgumentNullException.ThrowIfNull(personId, nameof(personId));

    // return the first personId that matches in the list or null if it can't find it
    return await _db.Persons
      .Where(p => p.PersonId == personId)
      .Select(p => p.ToPersonResponse())
      .FirstOrDefaultAsync();
  }

  public async Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string? searchString)
  {
    // get all of the persons
    List<PersonResponse> allPersons = await GetAllPersons();

    // list that will contain only the matching persons
    List<PersonResponse> matchingPersons = allPersons;

    // the searchBy string is empty so return all persons
    if (string.IsNullOrEmpty(searchBy))
    {
      return matchingPersons;
    }

    switch (searchBy)
    {
      // Get all of the person Names where it contains the searchstring
      case nameof(PersonResponse.PersonName):
        return allPersons.Where(p => p.PersonName?.Contains(searchString!, StringComparison.OrdinalIgnoreCase) ?? false).ToList();

      case nameof(PersonResponse.Email):
        return allPersons.Where(p => p.Email?.Contains(searchString!, StringComparison.OrdinalIgnoreCase) ?? false).ToList();

      case nameof(PersonResponse.PersonGender):
        return allPersons.Where(p => string.Equals(p.PersonGender, searchString, StringComparison.OrdinalIgnoreCase)).ToList();

      case nameof(PersonResponse.DateOfBirth):
        return allPersons.Where(p => p.DateOfBirth?.ToString("dd MMMM yyyy").Contains(searchString!, StringComparison.OrdinalIgnoreCase) ?? false).ToList();

      case nameof(PersonResponse.Age):
        return allPersons.Where(p => string.Equals(p.Age.ToString(), searchString, StringComparison.OrdinalIgnoreCase)).ToList();

      default:
        return allPersons;
    }

  }

  public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderEnum sortOrder)
  {

    // return original if no searchby was given
    if (string.IsNullOrEmpty(sortBy))
    {
      return allPersons;
    }

    // list we will return to the user
    List<PersonResponse> sortedPersons = sortBy switch
    {
      nameof(PersonResponse.PersonName) => allPersons.OrderBy(p => p.PersonName).ToList(),
      nameof(PersonResponse.Email) => allPersons.OrderBy(p => p.Email).ToList(),
      nameof(PersonResponse.DateOfBirth) => allPersons.OrderBy(p => p.DateOfBirth).ToList(),
      nameof(PersonResponse.PersonGender) => allPersons.OrderBy(p => p.PersonGender).ToList(),
      nameof(PersonResponse.Age) => allPersons.OrderBy(p => p.Age).ToList(),
      _ => allPersons
    };

    // sort in descending order if necessary
    if (sortOrder == SortOrderEnum.Descending)
    {
      sortedPersons.Reverse();
    }

    return sortedPersons;
  }

  public async Task<PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest)
  {
    // throw an error if the arg is null
    ArgumentNullException.ThrowIfNull(personUpdateRequest);

    // validate personUpdateRequest
    ValidationHelper.ModelValidation(personUpdateRequest);

    // grab the person from the list
    Person? personToUpdate = await _db.Persons.FirstOrDefaultAsync(p => p.PersonId == personUpdateRequest.PersonId);

    // the person by that id was not found
    if (personToUpdate == null)
    {
      throw new ArgumentException($"No person found with {personUpdateRequest.PersonId}");
    }

    // update all of the properties
    personToUpdate.PersonName = personUpdateRequest.PersonName;
    personToUpdate.Email = personUpdateRequest.Email;
    personToUpdate.Address = personUpdateRequest.Address;
    personToUpdate.DateOfBirth = personUpdateRequest.DateOfBirth;
    personToUpdate.PersonGender = personUpdateRequest.PersonGender;
    personToUpdate.CountryId = personUpdateRequest.CountryId;
    personToUpdate.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

    // update the persons
    await _db.Persons
        .Where(p => p.PersonId == personToUpdate.PersonId)
        .ExecuteUpdateAsync(p => p
            .SetProperty(x => x.PersonName, x => personToUpdate.PersonName)
            .SetProperty(x => x.Email, x => personToUpdate.Email)
            .SetProperty(x => x.Address, x => personToUpdate.Address)
            .SetProperty(x => x.DateOfBirth, x => personToUpdate.DateOfBirth)
            .SetProperty(x => x.PersonGender, x => personToUpdate.PersonGender)
            .SetProperty(x => x.CountryId, x => personToUpdate.CountryId)
            .SetProperty(x => x.ReceiveNewsLetters, x => personToUpdate.ReceiveNewsLetters));

    // save the changes in the db
    await _db.SaveChangesAsync();

    // return the object to the user
    return personToUpdate.ToPersonResponse();
  }

  public async Task<bool> DeletePerson(Guid? personId)
  {
    // personid is null throw exceptionGender
    ArgumentNullException.ThrowIfNull(personId);

    // get the person from the list
    Person? personToDelete = await _db.Persons.FirstOrDefaultAsync(p => p.PersonId == personId);


    if (personToDelete == null)
    {
      return false;
    }

    var result = _db.Persons.Remove(personToDelete);
    await _db.SaveChangesAsync();

    return true;
  }

  public async Task<MemoryStream> GetPersonsCSV()
  {
    // we will hold a stream in memory
    MemoryStream memoryStream = new MemoryStream();
    StreamWriter streamWriter = new StreamWriter(memoryStream);

    // configuration of how you will write to the csv file
    CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);

    // our writer stream
    CsvWriter csvWriter = new CsvWriter(streamWriter, csvConfiguration);

    // writes the header row columns
    csvWriter.WriteField(nameof(Person.PersonName));
    csvWriter.WriteField(nameof(Person.Email));
    csvWriter.WriteField(nameof(Person.DateOfBirth));
    csvWriter.WriteField(nameof(Person.PersonGender));
    csvWriter.WriteField(nameof(Person.Country));
    csvWriter.NextRecord();

    // csvWriter.WriteHeader<PersonResponse>();

    // get all of the persons from the db
    List<PersonResponse> persons = _db.Persons.Include(nameof(Person.Country)).Select(p => p.ToPersonResponse()).ToList();

    foreach (var p in persons)
    {
      csvWriter.WriteField(p.PersonName);
      csvWriter.WriteField(p.Email);
      csvWriter.WriteField(p!.DateOfBirth!.Value.ToString("yyyy-MM-dd"));
      csvWriter.WriteField(p!.PersonGender);
      csvWriter.WriteField(p!.Country);
      csvWriter.NextRecord();
      csvWriter.Flush();
    }

    // reset memory stream idx
    memoryStream.Position = 0;

    // return the memory stream
    return memoryStream;
  }

  public async Task<MemoryStream> GetPersonExcel(){
    MemoryStream memoryStream = new MemoryStream();
    using (ExcelPackage excelPackage = new ExcelPackage())
    {
      // add a new worksheet to the workbook
      ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("PersonsSheet");

      // set the header row
      excelWorksheet.Cells["A1"].Value = "Person Name";
      excelWorksheet.Cells["B1"].Value = "Email";
      excelWorksheet.Cells["C1"].Value = "Date of Birth";
      excelWorksheet.Cells["D1"].Value = "PersonGender";
      excelWorksheet.Cells["E1"].Value = "Country";

      // set the values 
      var rowIdx = 2;

      // get all of the persons in a list
      List<PersonResponse> persons = _db.Persons.Include(nameof(Person.Country)).Select(t => t.ToPersonResponse()).ToList();

      // go through all of the persons and add them to the persons worksheet
      foreach (var p in persons)
      {
        excelWorksheet.Cells[$"A{rowIdx}"].Value = p.PersonName;
        excelWorksheet.Cells[$"B{rowIdx}"].Value = p.Email;
        excelWorksheet.Cells[$"C{rowIdx}"].Value = p.DateOfBirth;
        excelWorksheet.Cells[$"D{rowIdx}"].Value = p.PersonGender;
        excelWorksheet.Cells[$"E{rowIdx}"].Value = p.Country;

      }

      // autofit the table
      excelWorksheet.Cells.AutoFitColumns();

      await excelPackage.SaveAsync();
    }

    memoryStream.Position = 0;
    return memoryStream;
  }
}
