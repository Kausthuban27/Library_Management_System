using AutoMapper;
using LibraryData.Models;
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
        }
    }
}
