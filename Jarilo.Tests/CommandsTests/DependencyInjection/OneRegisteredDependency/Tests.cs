using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.CommandsTests.DependencyInjection.OneRegisteredDependency
{
    public class Tests
    {
        [Fact]
        public async Task AllDependenciesRegistered()
        {
            await AppTest.ArrangeAndRun(
                Command.Name,
                app =>
                {
                    app.Services.AddSingleton<Service>();
                },
                output =>
                {
                    output
                        .AssertLength(1)
                        .AssertService();
                });
        }

        [Fact]
        public async Task DependenciesNotRegisteredAreNulls()
        {
            await AppTest.Run(Command.Name, output =>
            {
                output.AssertLength(0);
            });
        }
    }
}
