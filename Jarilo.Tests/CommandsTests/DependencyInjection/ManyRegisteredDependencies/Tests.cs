using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.CommandsTests.DependencyInjection.ManyRegisteredDependencies
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
                    app
                        .Services
                        .AddSingleton<Service1>()
                        .AddSingleton<Service2>();
                },
                output =>
                {
                    output
                        .AssertLength(2)
                        .AssertService1()
                        .AssertService2();
                });
        }

        [Fact]
        public async Task SomeDependenciesRegistered()
        {
            await AppTest.ArrangeAndRun(
                Command.Name,
                app =>
                {
                    app.Services.AddSingleton<Service1>();
                },
                output =>
                {
                    output
                        .AssertLength(1)
                        .AssertService1();
                });
        }

        [Fact]
        public async Task NoDependenciesRegistered()
        {
            await AppTest.Run(Command.Name, output =>
            {
                output.AssertLength(0);
            });
        }
    }
}
