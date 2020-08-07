namespace CustomerManagement.API.Dtos
{
    public class PaginationDto
    {
        public int Page { get; set; } = 1;

        private int recordsPerPage = 100;
        private readonly int maxRecordsPerPage = 100;

        public int RecordsPerPage
        {
            get
            {
                return recordsPerPage;
            }
            set
            {
                recordsPerPage = (value > maxRecordsPerPage) ? maxRecordsPerPage : value;
            }
        }
    }
}