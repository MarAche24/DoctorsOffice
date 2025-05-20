using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arztpraxis.Migrations
{
    /// <inheritdoc />
    public partial class addTreatmentInfoView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("IF OBJECT_ID('View_TreatmentInfo') IS NOT NULL BEGIN DROP VIEW View_TreatmentInfo END");
            migrationBuilder.Sql("""
                CREATE VIEW View_TreatmentInfo AS
                    SELECT Treatments.Id, People.FirstName AS PatientFirstName, People.LastName AS PatientLastName, PrescriptionDate AS StartDate, Memo
                        FROM dbo.Treatments
                        JOIN dbo.People ON Treatments.PatientId = People.Id;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("IF OBJECT_ID('View_TreatmentInfo') IS NOT NULL BEGIN DROP VIEW View_TreatmentInfo END");
        }
    }
}
