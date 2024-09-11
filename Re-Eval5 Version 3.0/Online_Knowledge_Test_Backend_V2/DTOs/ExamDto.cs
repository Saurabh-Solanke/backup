namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class ExamDto
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPublished { get; set; }
        public int Duration { get; set; }
        public int TotalMarks { get; set; }
        public int PassingMarks { get; set; }
        public string CreatedByUserId { get; set; }  // Added this to include the creator info in response
        public bool isRandmized { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
