using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
  public class PersonsDbContext : DbContext
  {
    public PersonsDbContext(DbContextOptions options) : base(options){
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Person> Persons { get; set; }

    // Creates the models and configures them
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // connect Countries class to the Countries table
      modelBuilder.Entity<Country>().ToTable("Countries")
        // establish the primary key
        .HasKey(c => c.CountryId);

      // connect Person class to the Persons table
      modelBuilder.Entity<Person>().ToTable("Persons")
        // establish the primary key
        .HasKey(p => p.PersonId);

      //Seed to Countries
      string countriesJson = System.IO.File.ReadAllText("countries.json");
      List<Country>? countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);

      foreach (Country country in countries ?? Enumerable.Empty<Country>())
        modelBuilder.Entity<Country>().HasData(country);

      //Seed to Persons
      string personsJson = System.IO.File.ReadAllText("persons.json");
      List<Person>? persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personsJson);

      foreach (Person person in persons ?? Enumerable.Empty<Person>())
        modelBuilder.Entity<Person>().HasData(person);

      // Fluent API, adds the TIN column
      var personFluent = modelBuilder.Entity<Person>();
      personFluent.Property(temp => temp.TIN)
        .HasColumnName("TaxIdentificationNumber")
        .HasDefaultValue("ABC123")
        .HasColumnType("varchar(10)");

      // setup the index
      // personFluent.HasIndex(p => p.TIN).IsUnique();
      
      // Add a Check Constraint to the table
      personFluent.ToTable(t => t.HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber]) > 1"));

      // Table relations

      // specify the table relation for the person entity
      modelBuilder.Entity<Person>(e => {
        e
          // person has 1 country
          .HasOne<Country>(p => p.Country)

          // the country has many persons
          .WithMany(c => c.Persons)

          // the connection is made with the foreignkey called CountryId
          .HasForeignKey(p => p.CountryId);
      });
    }

    public Task<List<Person>> sp_GetAllPersons() {
      // get the result set from the following query
      var persons = Persons.FromSqlRaw("Execute [dbo].[GetAllPersons]").ToListAsync();
      return persons;
    }

    public async Task<int> sp_InsertPerson(Person person) {

      // setting up the sql parameters
      SqlParameter[] sqlParameters = new SqlParameter[] {
        new SqlParameter("@PersonId", person.PersonId),
        new SqlParameter("@PersonName", person.PersonName),
        new SqlParameter("@Email", person.Email ?? (object)DBNull.Value),
        new SqlParameter("@Address", person.Address ?? (object)DBNull.Value),
        new SqlParameter("@DateOfBirth", person.DateOfBirth ?? (object)DBNull.Value),
        new SqlParameter("@PersonGender", person.PersonGender ?? (object)DBNull.Value),
        new SqlParameter("@CountryId", person.CountryId ?? (object)DBNull.Value),
        new SqlParameter("@ReceiveNewsLetters", person.ReceiveNewsLetters)
      };

      // returns the number of rows inserted
      return await Database.ExecuteSqlRawAsync("EXECUTE [dbo].[InsertPerson] @PersonId, @PersonName, @Email, @Address, @DateOfBirth, @PersonGender, @CountryId, @ReceiveNewsLetters", sqlParameters);
    }

  }
}