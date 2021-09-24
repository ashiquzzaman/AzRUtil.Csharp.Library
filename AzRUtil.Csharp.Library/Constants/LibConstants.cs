using System.ComponentModel.DataAnnotations;

namespace AzRUtil.Csharp.Library.Constants
{
    public static class LibConstants
    {
        #region LayoutType
        public enum FrontTheme
        {
            Default = 0,
            AzR,
            HK,
            OTTS,
            Quickai,
            Item1,
            Item2,
            Item3,
            Item4,
            Item5,
            Item6,
            Item7,
            Item8,
            Item9,
            Item10,

        }
        public enum ThemeBootstrap
        {
            V3 = 0,
            V4,
            V5
        }
        public enum AdminTheme
        {
            AdminLTE = 0,
            AdminAzR,
            AdminSLEEK,
            AdminMP,
            AdminDA,
            AdminFUSE,
            AdminFOCUS,
            AdminROBUST,
            AdminSTAR,
            AdminEASY,
            AdminFLAT2,
            AdminFLAT3,
            AdminBSBM,
            AdminPAGES,
            AdminMDP,
            AdminSING,
            AdminMONSTER,
            AdminLIMITLESS,
            AdminPORTO,
            AdminELEPHANT,
            AdminLIGHT,
            AdminCUBIC,
            AdminELITE,
            AdminCOLOR,
            AdminSB,
            AdminMETRONIC,
        }
        public enum LayoutType
        {
            MultiplePages = 0,
            SinglePage = 1,
            //  MixedPage = 2,
        }

        public enum SaveFormType
        {
            Redirect = 0,
            Modal = 1,
            // Load = 2,
        }

        public enum ContentType
        {
            Redirect = 0,
            Load = 1,
            // Replace = 2,
        }

        public enum ModalType
        {
            Normal = 0,
            Small = 1,
            Medium = 2,
            Large = 3,
            Auth = 10,
        }


        #endregion
        #region SETTING

        public enum AccessType
        {
            Public = 0,//all over
            Private,//only the user
            Protected,//share in same company
            Internal,//share in same group of company

        }
        public enum VisibilityLevel
        {
            Public = 0,//all over
            Private,//only the user
            Protected,//share in same company
            Internal,//share in same group of company

        }

        public enum ActionType
        {
            None = 0,
            Create = 1,
            Update = 2,
            Delete = 3,
            Cancel = 4,
            Active = 5,
            InActive = 6,
            Unchange = 7,

        }
        public enum AlertType
        {
            EveryTime = 1,
            Daily = 2,
            Weekly = 3,
            Monthly = 4,
            Yearly = 5

        }

        public enum DatabaseType
        {
            MSSQL = 0,
            MySQL,
            Oracle,
            PostgreSQL,
            SQLite,
            DB2,

        }
        public enum ExceptionType
        {
            Success = 0,
            Debug = 1,
            Info = 2,
            Warning = 3,
            Error = 4,
            Fatal = 5
        }
        public enum FieldType
        {
            Text = 1,
            Integer = 2,
            Decimal = 3,
            TextArea = 4,
            DropDown = 5,//relational key
            DateTime = 6,
            Date = 7,
            Time = 8,
            RadioButton = 9,
            CheckBox = 10,
            Image = 11,
            File = 12,
            BooleanRadioButton = 13,//Bool
            BooleanCheckBox = 14,//Bool
        }

        public enum FollowType
        {
            NONE = 0,
            Follow = 1,
            Referrer = 2,
            Supervisor = 3,
        }


        public enum Gender
        {

            // [Display(Name = "M")]
            Male = 1,
            //  [Display(Name = "F")]
            Female = 2,
            //[Display(Name = "E")]
            //Epicene = 3,


        }
        public enum ReportFormat
        {
            Html,
            Pdf,
            Excel,
            Word
        }

        public enum RequestType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public enum OnlineStatus
        {
            OffLine = 0,
            Online = 1,
            DoNotDisturb = 2,
            Invisible = 3

        }
        public enum Priority
        {

            High = 3,
            Medium = 2,
            Low = 1,
        }

        public enum SummaryDetails
        {
            [Display(Name = "Details")]
            Details = 0,
            [Display(Name = "Summary")]
            Summary = 1,

        }
        public enum SyncStatus
        {

            NotSync = 0,
            SyncUpdate = 1,
            TwoWaySync = 2,
            AllDeviceSync = 3,
            SyncFailed = 10,
        }
        public enum UniqueIdentifier
        {
            Email = 1,
            PhoneNumber = 2,
            UniqueName = 3,
            UserName = 4,
        }
        public enum SaveContinueType
        {
            None = 0,
            Add = 1,
            Edit = 2
        }
        public enum SenderOptions
        {
            ResetPassword = 0,
            ConfirmEmail = 1,
            Others = 100
        }
        #endregion


        public enum MonthOfYear
        {
            //None = 0,
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }
    }
}
