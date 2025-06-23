import React from 'react';
import { View, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, Dimensions } from 'react-native';
import { NavigationProp, useNavigation } from '@react-navigation/native';
import { useForm, FormProvider } from 'react-hook-form';
import { CustomText } from '../../components/CustomText';
import { CustomIcon } from '../../components/CustomIcon';
import { COLORS, FONT_PACIFICO, SHADOW } from '../../utils/theme';
import PetTypeSelector from './components/PetTypeSelector';
import DatePicker from './components/DatePicker';
import LocationSelector from './components/LocationSelector';
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from 'react-native-responsive-screen';
import { AuthStackNavigationType } from '../../utils/types/NavigationTypes';

const { height } = Dimensions.get('window');

const BoardingScreen = () => {
  const navigation = useNavigation<NavigationProp<AuthStackNavigationType>>();
  const methods = useForm({
    defaultValues: {
      petType: '',
      dates: null,
      location: '',
    },
  });

  const onSubmit = (data: any) => {
    console.log('Form Data:', data);
    navigation.navigate('Searching');
  };

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => navigation.goBack()}>
            <CustomIcon
              type="Ionicons"
              icon="arrow-back"
              size={24}
              color={COLORS.TextPrimary}
            />
          </TouchableOpacity>
          
        </View>
        <CustomText textType="BodyMediumRegular" color={COLORS.TextPrimary}>Skip</CustomText>
      </View>

      <FormProvider {...methods}>
        <ScrollView contentContainerStyle={styles.scrollContent} showsVerticalScrollIndicator={false}>
          <CustomText textStyle={styles.title} fontSize="S24" color={COLORS.TextPrimary}>
            Boarding
          </CustomText>

          <CustomText textType="BodyLargeSemiBold" color={COLORS.TextPrimary} style={styles.subtitle}>
            When do you need a sitter?
          </CustomText>

          <View style={styles.card}>
            <PetTypeSelector />
            <DatePicker />
            <LocationSelector />
          </View>

          <CustomText textType="SubtitleRegular" color={COLORS.NeutralGrey60} style={styles.footerNote}>
            Add dates and location to see sitters who'll be available for your need.
          </CustomText>
        </ScrollView>

        <View style={styles.footerButtonContainer}>
          <TouchableOpacity style={styles.nextButton} onPress={methods.handleSubmit(onSubmit)}>
            <CustomText textType="BodyLargeSemiBold" color={COLORS.StaticWhite}>Next</CustomText>
          </TouchableOpacity>
        </View>
      </FormProvider>
    </SafeAreaView>
  );
};

export default BoardingScreen;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: COLORS.StaticWhite,
  },
  header: {
    paddingHorizontal: wp('4%'),
    paddingVertical: hp('4.2%'),
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  headerLeft: {
    flexDirection: 'row',
    gap: wp('4%'),
  },
  scrollContent: {
    flexGrow: 1,
    justifyContent: 'center',
    paddingHorizontal: 26,
    paddingBottom: 32,
  },
  title: {
    fontFamily: FONT_PACIFICO.regularFont,
    textAlign: 'left',
    marginBottom: 10,
  },
  subtitle: {
    marginBottom: 20,
    textAlign: 'left',
  },
  card: {
    backgroundColor: COLORS.StaticWhite,
    borderRadius: 12,
    padding: 16,
    minHeight: height * 0.4, // 🚀 Increased card height
    justifyContent: 'space-between',
    ...SHADOW.medium, // applies your global shadow
  },
  footerNote: {
    marginTop: 20,
    textAlign: 'left',
  },
  footerButtonContainer: {
    padding: 16,
    backgroundColor: COLORS.StaticWhite,
  },
  nextButton: {
    backgroundColor: COLORS.Primary,
    borderRadius: 24,
    paddingVertical: 14,
    alignItems: 'center',
  },
});
