﻿using Microsoft.EntityFrameworkCore;

namespace BlazorServerProject.Data
{
    public class AppContext:DbContext
    {
        public AppContext() { }
        public AppContext(DbContextOptions<AppContext> options) :
              base(options)  { }
    }
}
