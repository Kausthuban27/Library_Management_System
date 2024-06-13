using AutoMapper;
using LibraryData.Model;
using LibraryData.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Utilities
{
    public class MapperHelper : Profile
    {
        public MapperHelper() 
        {
            CreateMap<AddNewLibrarian, Librarian>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfJoining, opt => opt.Ignore());

            CreateMap<AddNewStudent, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<LibrarianAddBook, BookDetail>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<BookDetail, BookIssue>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IssueDate, opt => opt.Ignore());

            CreateMap<Librarian, AddNewLibrarian>();
        }
    }
}
