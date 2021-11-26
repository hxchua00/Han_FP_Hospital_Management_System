namespace HospitalManagementWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillID = c.Int(nullable: false, identity: true),
                        GST = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.BillID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        PatientName = c.String(),
                        PatientWeight = c.Double(nullable: false),
                        PatientHeight = c.Double(nullable: false),
                        PatientGender = c.Int(nullable: false),
                        PatientAge = c.Int(nullable: false),
                        Ethnicity = c.Int(nullable: false),
                        Address = c.String(),
                        ContactNum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatientID);
            
            CreateTable(
                "dbo.PatientVisitRecords",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DoctorInCharge = c.Int(nullable: false),
                        Department = c.Int(nullable: false),
                        Ward = c.Int(nullable: false),
                        BillInformation_BillID = c.Int(),
                        PatientID_PatientID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Bills", t => t.BillInformation_BillID)
                .ForeignKey("dbo.Patients", t => t.PatientID_PatientID)
                .Index(t => t.BillInformation_BillID)
                .Index(t => t.PatientID_PatientID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        HashedPassword = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientVisitRecords", "PatientID_PatientID", "dbo.Patients");
            DropForeignKey("dbo.PatientVisitRecords", "BillInformation_BillID", "dbo.Bills");
            DropIndex("dbo.PatientVisitRecords", new[] { "PatientID_PatientID" });
            DropIndex("dbo.PatientVisitRecords", new[] { "BillInformation_BillID" });
            DropTable("dbo.Users");
            DropTable("dbo.PatientVisitRecords");
            DropTable("dbo.Patients");
            DropTable("dbo.Bills");
        }
    }
}
