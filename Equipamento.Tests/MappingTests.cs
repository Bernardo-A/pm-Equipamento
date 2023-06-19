using AutoMapper;
using Equipamento.API.AutoMapperProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Externo.Tests
{
    public class MappingTests
    {
        [Fact]
        public void Setup()
        {
            MapperConfiguration mapperConfiguration = new(cfg =>
            {
                cfg.AddProfile(new EquipamentoAutoMapperProfile());
            }); ;

            IMapper mapper = new Mapper(mapperConfiguration);

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }

}
