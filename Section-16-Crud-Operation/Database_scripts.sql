select
  c.name,
  p.PersonName
from 
  persons p
  inner join Countries c 
  on p.CountryId = c.CountryId

