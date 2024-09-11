using Online_Knowledge__Test_Backend.Models;

namespace Online_Knowledge__Test_Backend.RepositoryLayer
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllSubjects();

    }
}
