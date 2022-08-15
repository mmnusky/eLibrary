namespace eLibrary.Shared
{
    public static class Constant
    {
        #region JWT Authentication
        public const string key = "superSecretKey@345";
        public const string ValidIssuer = "https://localhost:7216";
        public const string ValidAudience = "https://localhost:7216";
        public const string Bearer = "Bearer";
        #endregion

        #region Role Controller
        public const string AuthorStatus = "Author status has been changed successfully";
        public const string EmptyOrNullUser = "User id is null or empty";
        public const string StatusChangeError = "Error occur while changing the status of the author";
        public const string NoAuthorFound = "No authors found";
        public const string NoActiveAuthorFound = "No active authors found";
        public const string GetAllAuthorError = "Error while getting all authors";
        public const string GetActiveAuthorError = "Error while getting active authors";
        #endregion

        #region Roles
        public const string Author = "Author";
        public const string Admin = "Admin";
        public const string AdminAuthor = "Admin,Author";
        #endregion

        #region Book Details Controller
        public const string GetAllBookError = "Error while getting all book details";
        public const string UpdateBookError = "Error while updating book details";
        public const string DelateBookError = "Error while deleting book details";
        public const string InsertBookDetailslError = "Error while inserting book details";
        public const string GetByIdError = "Error while get employee";
        #endregion

        #region Account Controller
        public const string InvalidCredential = "In valid credentials have been provided";
        public const string BlockedUser = "User has been blocked by administrator";
        #endregion
    }
}
