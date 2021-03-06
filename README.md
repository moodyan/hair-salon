# _Hair Salon_

#### _App created for Epicodus Independent Project, C-Sharp - Week Three. Practice using SQL Database Basics. June 9, 2017_

#### By _**Alyssa Moody**_

## Description

_A simple app that allows salon employees to add/view/delete stylists and add/view/edit/delete clients from the database._

## Program Specifications

| Description  | Input Example | Output Example |
| ------------- | ------------- | ------------- |
| 1. The program allows employees to add a new stylist to the database.  | New Stylist Name: "Emmylou Earnest" Stylist specialty: "Men's cuts and short hair styles."  | "Success! You have added a new stylist."  |
| 2. The program allows users to view all stylists.  | --   | "Here is a list of all stylists."  |
| 3. The program allows employees to add a new client to a stylist in the database.  | New Client Name: "Beau Blue" Client details: "None."  | "Success! You have added a new client."  |
| 4. The program allows users to click on each stylist and view their details and which clients belong to that stylist.  | --   | "Stylist: Betty Jean, Specialty: Long hair, ombre coloring." "Her Clients: Darla Darling, Minnie Moo."  |
| 5. The program allows users to click on each client and see details about that client.  | --   | "Client: Darla Darling. Her Stylist: Betty Jean. Details: On her last visit (1/2/2013), we trimmed her hair and touched up her roots with dark drown."  |
| 6. The program allows employees to update an existing client's name and/or details.  | Client's Name (if changed): "" Client details: "On last visit, Minnie went from long hair to a short bob with bangs."  | "Success!"  |
| 7. The program allows employees to delete an existing client from the database.  | "Delete?"  | "Success!"  |
| 8. The program allows employees to delete an existing stylist from the database.  | "Delete?"  | "Success!"  |

## Setup/Installation Requirements

_Runs on the .Net Framework._

_Install Visual Studio 2015. https://go.microsoft.com/fwlink/?LinkId=532606 ._

_Install ASP.Net 5. This will give you access to the .NET Framework. https://go.microsoft.com/fwlink/?LinkId=627627 ._

_Restart PowerShell. While located in your machine's Home directory, enter the command > dnvm upgrade._

_Requires Nancy Web Framework located at: http://nancyfx.org/. You can also do this via Windows PowerShell with the command > **Install-Package Nancy**_

_**From GitHub: Download or clone project repository onto desktop from GitHub.**_

_In SQLCMD run: > CREATE DATABASE hair_salon; > GO > USE hair_salon; > GO_

_In your preferred database management system (I use SSMS), open the band_tracker.sql file from the project folder. Run the execute command on the file. If this does not work, run the following command in SQLCMD:

CREATE DATABASE hair_salon; GO USE hair_salon; GO CREATE TABLE clients (id INT IDENTITY(1,1), client_name VARCHAR(100), details VARCHAR(255), stylist_id INT); GO CREATE TABLE stylists (id INT IDENTITY(1,1), stylist_name VARCHAR(100), specialty VARCHAR(255)); GO_

_To create test database, in your preferred database management system (I use SSMS) open the hair_salon_test.sql file from the project folder. Run the execute command on the file. If this does not work, back up and restore the database as a test database in your preferred database management system.

_In PowerShell, cd into the project folder. Enter the command > **dnu restore**_

_Enter the command > **dnx kestrel**_

_In your preferred browser, navigate to http://localhost:5004/ and you should see the application._

## Known Bugs

_No known bugs._

## Support and contact details

_If you run into any issues or have questions, ideas or concerns, please contact Alyssa Moody at alyssanicholemoody@gmail.com_

## Technologies Used

_**Languages:** HTML, CSS, C#, SQL._

_**Frameworks:** Nancy, .Net._

_**Testing:** xUnit._

### License

*MIT license Agreement*

Copyright (c) 2017 **_Alyssa Moody_**
