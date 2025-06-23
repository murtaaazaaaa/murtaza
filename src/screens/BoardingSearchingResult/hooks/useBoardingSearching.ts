import { useState, useEffect } from 'react';

export interface Profile {
  id: string;
  name: string;
  title: string;
  description: string;
  distance: string;
  rating: number;
  reviewCount: number;
  repeatClients: number;
  price: number;
  profileImage: any;
  availabilityUpdated: string;
  isStarSitter?: boolean;
}

export const useBoardingSearching = () => {
  const [profiles, setProfiles] = useState<Profile[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Simulate API call
    const fetchProfiles = async () => {
      setLoading(true);
      
      // Mock data - in real app this would come from API
      const mockProfiles: Profile[] = [
        {
          id: '1',
          name: 'America C.',
          title: 'Star Sitter',
          description: 'LOTS OF SPACE to RO...',
          distance: '3.0 mi. away',
          rating: 5.0,
          reviewCount: 109,
          repeatClients: 40,
          price: 40,
          profileImage: require('../../../../assets/images/profile1.png'),
          availabilityUpdated: 'yesterday',
          isStarSitter: true,
        },
        {
          id: '2',
          name: 'Fortune C.',
          title: '',
          description: 'Happy to sit while you...',
          distance: '0.3 mi. away',
          rating: 5.0,
          reviewCount: 8,
          repeatClients: 6,
          price: 34,
          profileImage: require('../../../../assets/images/profile2.png'),
          availabilityUpdated: 'yesterday',
        },
        {
          id: '3',
          name: 'Cindy U.',
          title: '',
          description: 'Experienced pet sitter...',
          distance: '1.2 mi. away',
          rating: 4.8,
          reviewCount: 25,
          repeatClients: 12,
          price: 35,
          profileImage: require('../../../../assets/images/profile3.png'),
          availabilityUpdated: 'today',
        },
      ];
      
      setTimeout(() => {
        setProfiles(mockProfiles);
        setLoading(false);
      }, 1000);
    };

    fetchProfiles();
  }, []);

  const refreshProfiles = () => {
    setLoading(true);
    // Simulate refresh
    setTimeout(() => {
      setLoading(false);
    }, 500);
  };

  return {
    profiles,
    loading,
    refreshProfiles,
  };
};