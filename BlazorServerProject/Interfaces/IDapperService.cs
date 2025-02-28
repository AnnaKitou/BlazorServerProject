﻿using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace BlazorServerProject.Interfaces
{
    public interface IDapperService
    {
        DbConnection GetConnection();
        T Get<T>(string sp, DynamicParameters parms,
           CommandType commandType = CommandType.StoredProcedure);
        List<T> GetAll<T>(string sp, DynamicParameters parms,
           CommandType commandType = CommandType.StoredProcedure);
        int Execute(string sp, DynamicParameters parms,
           CommandType commandType = CommandType.StoredProcedure);
        T Insert<T>(string sp, DynamicParameters parms,
           CommandType commandType = CommandType.StoredProcedure);
        T Update<T>(string sp, DynamicParameters parms,
           CommandType commandType = CommandType.StoredProcedure);
    }
}
