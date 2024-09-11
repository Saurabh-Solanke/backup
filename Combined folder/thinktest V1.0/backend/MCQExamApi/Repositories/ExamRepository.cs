using MCQExamApi.Data;
using MCQExamApi.Dtos.Exam;
using MCQExamApi.interfaces;
using Microsoft.EntityFrameworkCore;

namespace MCQExamApi.Repositories
{
    public class ExamRepository : IExamRepository
    {
        private readonly ExamContext _examContext;
        public ExamRepository(ExamContext context) {
            _examContext = context;
        }

        public Task<ExamRespDTO> GetExamDetailsById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
