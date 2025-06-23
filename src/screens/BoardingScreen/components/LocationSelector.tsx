import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';
import { useBoardingForm } from '../hooks/useBoardingForm';

const LocationSelector = () => {
  const { location, setLocation } = useBoardingForm();

  return (
    <TouchableOpacity style={styles.row}>
      <Text style={styles.label}>Your Location</Text>
      <Text style={styles.value}>{location || 'Select your location'}</Text>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  row: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingVertical: 14,
    borderTopWidth: 1,
    borderColor: '#ddd',
  },
  label: {
    fontSize: 16,
    fontWeight: '600',
  },
  value: {
    fontSize: 14,
    color: '#999',
  },
});

export default LocationSelector;
