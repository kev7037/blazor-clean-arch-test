# Blazor app with clean architecture and CQRS with sqlserver as database

This Project is simple CRUD application with ASP NET that implements the below model:
```
Customer {
	Firstname
	Lastname
	DateOfBirth
	PhoneNumber
	Email
	BankAccountNumber
}
```
## Used Practices and patterns:

- [Clean architecture](https://github.com/jasontaylordev/CleanArchitecture)
- [CQRS](https://en.wikipedia.org/wiki/Command%E2%80%93query_separation#Command_query_responsibility_separation) pattern ([Event sourcing](https://en.wikipedia.org/wiki/Domain-driven_design#Event_sourcing)).

### Extras (Validations)

- Used ([Google LibPhoneNumber](https://github.com/google/libphonenumber) to validate number at the backend.

- Email and a Bank account number (IBAN) validation happens before submitting the form.

- Customers is unique in database: By `Firstname`, `Lastname` and `DateOfBirth`.

- Email must be unique in the database.
