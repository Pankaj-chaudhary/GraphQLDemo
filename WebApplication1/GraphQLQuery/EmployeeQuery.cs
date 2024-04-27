﻿using GraphQL;
using GraphQL.Types;
using System.Xml.Linq;
using WebApplication1.Model;
using WebApplication1.Service;

namespace WebApplication1.GraphQLQuery
{
    public class EmployeeQuery : ObjectGraphType
    {
        public EmployeeQuery(IEmployeeService employeeService)
        {
            Field<ListGraphType<EmployeeDetailsType>>(Name = "Employees", resolve: x => employeeService.GetEmployees());
            Field<ListGraphType<EmployeeDetailsType>>(Name = "Employee",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: x => employeeService.GetEmployee(x.GetArgument<int>("id")));
        }
    }

    public class EmployeeDetailsSchema : Schema
    {
        public EmployeeDetailsSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<EmployeeQuery>();
        }
    }
}
