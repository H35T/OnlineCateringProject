Scaffolding has generated all the files and added the required dependencies.

change value string OnlineCateringDB in appsettings.json 
change value FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.HOANNC\MSSQL\DATA\OnlineCateringDB.mdf' with absolute path
and run script.sql after that 
run
Scaffold-DbContext Name=OnlineCateringDB Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context OnlineCateringContext -Force


login with admin : admin/1
login with customer: customer/1
login with caterer : hoannb/hoannbnc11

content
<div class="page-content">
    <div class="container">
        <div class="row">

        </div>
    </div>
</div>
