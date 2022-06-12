using BlazorServerProject.Entities;
using BlazorServerProject.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorServerProject.Data
{
    public class DocumentService : IDocumentService
    {
        private readonly IDapperService _dapperService;
        public DocumentService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }
        public Task<int> Create(Document document)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Name", document.Name, DbType.String);
            dbPara.Add("StatuCode", document.StatusCode, DbType.Int32);
            dbPara.Add("Freeze", document.Freeze, DbType.Int32);
            var documentId = Task.FromResult
               (_dapperService.Insert<int>("[dbo].[spAddDocument]",
               dbPara, commandType: CommandType.StoredProcedure));
            return documentId;
        }
        public Task<Document> GetById(int id)
        {
            var document = Task.FromResult
               (_dapperService.Get<Document>
               ($"select * from [Documents] where Id = {id}", null,
               commandType: CommandType.Text));
            return document;
        }
        public Task<int> Delete(int id)
        {
            var deleteDocument = Task.FromResult
               (_dapperService.Execute
               ($"Delete [Documents] where Id = {id}", null,
               commandType: CommandType.Text));
            return deleteDocument;
        }
        public Task<int> Count(string search)
        {
            var totDocument = Task.FromResult(_dapperService.Get<int>
               ($"select COUNT(*) from [Documents] WHERE Name like '%{search}%'", null,
                  commandType: CommandType.Text));
            return totDocument;
        }
        public Task<List<Document>> ListAll(int skip, int take,
         string orderBy, string direction = "DESC", string search = "")
        {
            var documents = Task.FromResult
               (_dapperService.GetAll<Document>
               ($"SELECT * FROM [Documents] WHERE Name like '%{search}%' ORDER BY {orderBy} { direction} OFFSET { skip}  ROWS FETCH NEXT { take} ROWS ONLY; ", null, commandType: CommandType.Text));
         return documents;
        }

        public Task<int> Update(Document document)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", document.Id);
            dbPara.Add("Name", document.Name, DbType.String);
            dbPara.Add("StatuCode", document.StatusCode, DbType.Int32);
            dbPara.Add("Freeze", document.Freeze, DbType.Int32);
            var updateDocument = Task.FromResult
               (_dapperService.Update<int>("[dbo].[spUpdateDocument]",
               dbPara, commandType: CommandType.StoredProcedure));
            return updateDocument;
        }
    }
}
