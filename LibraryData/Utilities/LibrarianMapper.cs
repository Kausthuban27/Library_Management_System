using AutoMapper;
using LibraryData.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Utilities
{
    public static class LibrarianMapper
    {
        private static IMapper? _mapper;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            if (_mapper == null)
            {
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }
        }
        public static T MapLibrarian<T>(AddNewLibrarian librarian)
        {
            if (_mapper == null)
            {
                throw new InvalidOperationException("Mapper is not initialized. Call Initialize method first.");
            }
            return _mapper.Map<T>(librarian);
        }
    }
}
