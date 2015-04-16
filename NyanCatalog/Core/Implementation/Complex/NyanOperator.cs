namespace Core.Implementation.Complex 
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure;
    using Models;
    using DataRepository.Models;
    using Mapping;

    public class NyanOperator : BaseCrud<NyanDTO, Nyan> 
    {
        public NyanOperator(NyanEntities entities) : base(entities) {
            NyanMapping.CreateMappings(); // Ensure mappings exist!
        }
    }
}
