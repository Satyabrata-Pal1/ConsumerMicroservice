using ConsumerAPI.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerAPI
{
    public class BusinessMaster
    {
        private readonly List<BusinessDTO> permissibleBusinesses;
        public BusinessMaster()
        {
            permissibleBusinesses = new List<BusinessDTO>
            {
                new BusinessDTO
                {
                    BusinessType= BusinessDTO.BusinessTypes.Agricultire,
                    BuisnessTurnover =5000
                },
                new BusinessDTO
                {
                    BusinessType= BusinessDTO.BusinessTypes.ConsumerGoods,
                    BuisnessTurnover =30000
                },
                 new BusinessDTO
                {
                    BusinessType= BusinessDTO.BusinessTypes.Production,
                    BuisnessTurnover =100000
                },
                 new BusinessDTO
                {
                    BusinessType= BusinessDTO.BusinessTypes.Assembly,
                    BuisnessTurnover =20000,
                },
                 new BusinessDTO
                {
                    BusinessType= BusinessDTO.BusinessTypes.Development,
                    BuisnessTurnover =20000,
                },
                 new BusinessDTO
                {
                    BusinessType= BusinessDTO.BusinessTypes.Construction,
                    BuisnessTurnover =30000,
                }

            };
        }

        public bool IsValidBuisness(BusinessDTO business)
        {
            bool flag = false;
            foreach(BusinessDTO permissibleBusiness in permissibleBusinesses)
            {
                if (business.BusinessType == permissibleBusiness.BusinessType && business.BuisnessTurnover >= permissibleBusiness.BuisnessTurnover)
                {
                    flag = true;
                    break;
                }
                   
            }

            return flag;
        }
    }
}
