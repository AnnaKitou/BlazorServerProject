using BlazorServerProject.Entities;
using BlazorServerProject.Interfaces;
using Dapper;
using System;
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
            dbPara.Add("name", document.Name, DbType.String);
            dbPara.Add("statusCode", document.StatusCode, DbType.Int32);
            dbPara.Add("freeze", document.Freeze, DbType.Int32);
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
        public Task<int> Count(string search, string searchWord)
        {
          
            int c = Convert.ToInt32(search);
            if (c==-1)
            {
               var totDocument = Task.FromResult(_dapperService.Get<int>
                               ($"SELECT COUNT(*) FROM [Documents] WHERE Name like '%{searchWord}%'; ", null, commandType: CommandType.Text));
                return totDocument;
            }
            else
            {
                
               var totDocument = Task.FromResult(_dapperService.Get<int>
                      ($"select COUNT(*) from [Documents] WHERE Name like '%{searchWord}%' ; ", null,
                         commandType: CommandType.Text));
                return totDocument;
            }
        }
        public Task<List<Document>> ListAll(int skip, int take,
         string orderBy, string direction = "DESC", string search ="-1",string searchWord="")
        {

            int c = Convert.ToInt32(search);

            if (c ==-1 )
            {
                var documents = Task.FromResult
                               (_dapperService.GetAll<Document>
                               ($"SELECT * FROM [Documents] WHERE Name like '%{searchWord}%' ORDER BY {orderBy} { direction} OFFSET { skip}  ROWS FETCH NEXT { take} ROWS ONLY; ", null, commandType: CommandType.Text));
                return documents;
            }
            else
            {

                var documents = Task.FromResult
                    (_dapperService.GetAll<Document>
                    ($"SELECT * FROM [Documents] WHERE Name like '%{searchWord}%' AND StatusCode={c} ORDER BY {orderBy} { direction} OFFSET { skip}  ROWS FETCH NEXT { take} ROWS ONLY; ", null, commandType: CommandType.Text));
                return documents;
            }
        }

        public Task<int> Update(Document document)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", document.Id);
            dbPara.Add("name", document.Name, DbType.String);
            dbPara.Add("statusCode", document.StatusCode, DbType.Int32);
            dbPara.Add("freeze", document.Freeze, DbType.Int32);
            var updateDocument = Task.FromResult
               (_dapperService.Update<int>("[dbo].[spUpdateDocs]",
               dbPara, commandType: CommandType.StoredProcedure));
            return updateDocument;
        }
    }
}
