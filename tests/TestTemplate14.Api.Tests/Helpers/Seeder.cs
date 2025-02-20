using System.Collections.Generic;
using TestTemplate14.Core.Entities;
using TestTemplate14.Data;

namespace TestTemplate14.Api.Tests.Helpers
{
    public static class Seeder
    {
        public static void Seed(this TestTemplate14DbContext ctx)
        {
            ctx.Foos.AddRange(
                new List<Foo>
                {
                    new ("Text 1"),
                    new ("Text 2"),
                    new ("Text 3")
                });
            ctx.SaveChanges();
        }
    }
}
