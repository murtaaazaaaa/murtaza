import React from 'react';
import {TouchableOpacity} from 'react-native';
import {widthPercentageToDP as wp} from 'react-native-responsive-screen';
import { 
  AntDesign, 
  Entypo, 
  EvilIcons, 
  Feather, 
  FontAwesome, 
  FontAwesome5, 
  Fontisto, 
  Foundation, 
  Ionicons, 
  MaterialCommunityIcons, 
  MaterialIcons, 
  Octicons, 
  SimpleLineIcons,
  FontAwesome6
} from '@expo/vector-icons';
import { COLORS } from '../../utils/theme';
import {CustomIconProps} from './interface';
import {RFValue} from 'react-native-responsive-fontsize';


export const CustomIcon: React.FC<CustomIconProps> = ({
  icon,
  type,
  color,
  onPress,
  size,
  disabled,
  resizeMode,
  style,
  onLongPress,
  tintColor,
  iconStyle,
  ...rest
}) => {
  return (
    <TouchableOpacity
      style={style}
      onPress={onPress}
      onLongPress={onLongPress}
      disabled={
        disabled ||
        (typeof onPress !== 'function' && typeof onLongPress !== 'function')
      }>
      {type == 'Entypo' && (
        <Entypo
          disabled
          
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
      {type == 'Ionicons' && (
        <Ionicons
          disabled
          name={icon} 
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
      {type == 'FontAwesome' && (
        <FontAwesome
          disabled
          name={icon}
          style={iconStyle}
          
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
      {type == 'FontAwesome6' && (
        <FontAwesome6
          disabled
          name={icon}
          style={iconStyle}          
          
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
      {type == 'Fontisto' && (
        <Fontisto
          disabled
          
          style={iconStyle}          
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
      {type == 'FontAwesome5' && (
        <FontAwesome5
          disabled
          name={icon}
          style={iconStyle}          
          
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
      {type == 'Feather' && (
        <Feather
          disabled
          style={iconStyle}          
          
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
      {type == 'MaterialIcons' && (
        <MaterialIcons
          disabled
          style={iconStyle}          
          
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
      {type == 'MaterialCommunityIcons' && (
        <MaterialCommunityIcons
          disabled
          style={iconStyle}          
          
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
      {type == 'Foundation' && (
        <Foundation
          
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
      {type == 'AntDesign' && (
        <AntDesign
          
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
      {type == 'SimpleLineIcons' && (
        <SimpleLineIcons
          
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
      {type == 'EvilIcons' && (
        <EvilIcons
          
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
      {type == 'Octicons' && (
        <Octicons
          
          color={color || COLORS.NeutralGrey100}
          size={size || RFValue(20)}
          {...rest}
        />
      )}
    </TouchableOpacity>
  );
};
