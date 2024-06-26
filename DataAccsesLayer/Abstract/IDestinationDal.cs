﻿using EntityLayer.Concrete;

namespace DataAccsesLayer.Abstract
{
    public interface IDestinationDal:IGenericDal<Destination>
    {
        int getDestinationCount();

        Destination getDestinationByCityName(string name);
        void deleteDestinationByCityName(string name);

        List<Destination> getFourLastDestination();

        List<Destination> getDestinationBySearchFilter(int CityID,string dateTime);
    }
}
