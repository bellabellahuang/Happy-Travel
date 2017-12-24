using System.Collections.Generic;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace HappyTravel.MyFragment
{
    public class TabFragmentPagerAdapter : FragmentPagerAdapter
    {
        private List<Fragment> fragments;
        public TabFragmentPagerAdapter(FragmentManager fm, List<Fragment> fragments):base(fm)
        {
            
            this.fragments = fragments;
        }

        public override int Count
        {
            get
            {
                return fragments.Count;
            }
        }

        public override Fragment GetItem(int position)
        {
            return fragments[position];
        }
    }
}
