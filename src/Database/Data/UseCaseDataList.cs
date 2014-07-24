using EdFiValidation.ApiProxy.Core.Models;
using System;
using System.Collections.Generic;

namespace Database.Data
{
    public class UseCaseDataList : IDataList<UseCase>
    {
        public IEnumerable<UseCase> GetList()
        {
            return new List<UseCase> { 
                    new UseCase {
                        Id = new Guid("838bd241-f3c3-43b6-acc8-a372003f413f"),
                        Title = "Use Case 1",
                        Description = "Test validation use case meta data.",
                        IsOrdered = false,
                        Items = new List<UseCaseItem> {
                                    new UseCaseItem {
                                        Id = new Guid("a00c2daf-a8a1-477d-85cc-a372003f7f04"),
                                        Path = "/student",
                                        Method = "POST"
                                    },
                                    new UseCaseItem {
                                        Id = new Guid("1a7892c6-fdcc-4864-b2c8-a372003f7f04"),
                                        Path = "/class",
                                        Method = "POST"
                                    },
                                    new UseCaseItem {
                                        Id = new Guid("afde957d-897b-4ea4-8a5d-a372003f7f04"),
                                        Path = "/schedule",
                                        Method = "POST"
                                    }
                        }
                    },
                new UseCase {
                    Id = new Guid("3f72db83-a531-4a70-aaf5-a372003f413f"),
                    Title = "Use Case 2",
                    Description = "Test validation use case meta data.",
                    IsOrdered = false,
                    Items = new List<UseCaseItem> {
                                new UseCaseItem {
                                    Id = new Guid("6e9f64f5-4791-4442-8717-a372003f7f04"),
                                    Path = "/parent",
                                    Method = "POST"
                                },
                                new UseCaseItem {
                                    Id = new Guid("229793ab-1844-4329-9c7c-a372003f7f04"),
                                    Path = "/parent",
                                    Method = "POST"
                                },
                                new UseCaseItem {
                                    Id = new Guid("45a4b95a-b7ea-4a9b-b54f-a372003f7f04"),
                                    Path = "/parent",
                                    Method = "POST" 
                                }
                    }
                }
            };
        }
    }
}
