using BlazorServerProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServerProject.Interfaces
{
    public interface IDocumentService
    {
        Task<int> Create(Document document);
        Task<int> Delete(int Id);
        Task<int> Count(string search);
        Task<int> Update(Document document);
        Task<Document> GetById(int Id);
        Task<List<Document>> ListAll(int skip, int take,
            string orderBy, string direction, string search);
    }
}
