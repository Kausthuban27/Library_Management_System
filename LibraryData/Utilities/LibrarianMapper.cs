using AutoMapper;
using LibraryData.Models;
using LibraryData.Services;
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
        private static IMapper _mapper = null!;

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
        public static T MapStudent<T>(AddNewStudent student)
        {
            if (_mapper == null)
            {
                throw new InvalidOperationException("Mapper is not initialized. Call Initialize method first.");
            }
            return _mapper.Map<T>(student);
        }
        public static T MapBookIssued<T>(BookDetail book)
        {
            if (_mapper == null)
            {
                throw new InvalidOperationException("Mapper is not initialized. Call Initialize method first");
            }
            return _mapper.Map<T>(book);
        }
        public static T MapNewBook<T>(LibrarianAddBook book)
        {
            if (_mapper == null)
            {
                throw new InvalidOperationException("Mapper is not initialized. Call Initialize method first");
            }
            return _mapper.Map<T>(book);
        }
        public static T MapRetrievedLibrarian<T>(Librarian librarian)
        {
            if(_mapper == null)
            {
                throw new InvalidOperationException("Mapper is not initialized. Call Initialize method first");
            }
            return _mapper.Map<T>(librarian);
        }
    }
}
