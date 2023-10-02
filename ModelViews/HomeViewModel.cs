﻿using BethanysPieShop.Models;

namespace BethanysPieShop.ModelViews
{
    public class HomeViewModel
    {

        public IEnumerable<Pie> PiesOfTheWeek  { get; }
        public HomeViewModel(IEnumerable<Pie> piesOfTheWeek)
        {

                PiesOfTheWeek = piesOfTheWeek;
        }
    }
}
