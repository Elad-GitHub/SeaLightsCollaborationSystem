Design:

1.Please edit the connection string server name to your local sql server - the app builds a db and fetching/pushing from/to it.

2.Run in Pacakage Manager Console 
	1. Add-Migration InitialCreate
	2. Update-Database

DB:
two tables

1. Note - 	id (primery identity key), 
			reportID (the parent report id - connects to report table (assumed to be existed) 1 (report) to many (notes)), 
			cordinatorId (the user id - connects to user table (assumed to be existed) many to many),
			privacy (access to the note - can be private or public, and is open for extension because it is mapped to an enum),
			Title,
			Text

2. File - 	id (primery identity key), 
			parentID (the parent id - can be a reportId or a noteId - connects to report table (assumed to be existed) and to note table), 
			cordinatorId (the user id - connects to user table (assumed to be existed) many to many),
			privacy (access to the note - can be private or public, and is open for extension because it is mapped to an enum),
			Link (the path of the file location)
			
Class Diagram:

Models:
Attachment is a base class of note and file - they share properties.
Note - mapped to note table row
File - mapped to file table row
PrivacyType - Enum for access level for an attachment

Data Access Layer:
I used .Net Core Entity framework as a Data access layer to db.
The Context expose the tables.

Storage:
I could do the repository a more generic one and support also other uploading and downloading files methods, for example uploading and downloading from S3 Storage.
I didnt have enough time so i implemented a local storage.

Services:
all the api methods are using a deticated services. - NoteDataService and FileDataService which implements an interface.

I used the depndency injection logic for registering the services. and they are lazy created once (singleton) only when needed.

Testing:
didnt have enuogh time for testing upload and download files api methods but all the other web api methods are tested and passed.

I used UNIX for unit testing and created Mocks. 



 


 
 

			
			