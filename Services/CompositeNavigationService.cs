﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.Services
{
    public class CompositeNavigationService
    {
        private readonly IEnumerable<INavigationService> _navigationServices;

        public CompositeNavigationService(params INavigationService[] navigationServices)
        {
            _navigationServices = navigationServices;
        }

        public void Navigate()
        {
            foreach (INavigationService navigationService in _navigationServices)
            {
                navigationService.Navigate();
            }
        }
    }
}
