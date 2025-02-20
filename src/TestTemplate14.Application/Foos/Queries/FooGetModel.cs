using System;
using AutoMapper;
using TestTemplate14.Core.Entities;

namespace TestTemplate14.Application.Questions.Queries
{
    public class FooGetModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public class FooGetModelProfile : Profile
        {
            public FooGetModelProfile()
            {
                CreateMap<Foo, FooGetModel>();
            }
        }
    }
}
