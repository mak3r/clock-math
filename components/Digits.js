import React from "react";
import { StyleSheet, View, Image } from "react-native";

const BLOCK_W = 50;
const BLOCK_H = 50;

class Digit extends React.Component{
  render() {
    const x = this.props.position[0] - BLOCK_W / 2;
    const y = this.props.position[1] - BLOCK_H / 2;
    return (
      <View style={[styles.container, { left: x, top: y }]} >
          <Image
          style={styles.digit}
          source={require('../assets/clock/numbers/one.png')}
        />
      </View>
    );
  }
}

const styles = StyleSheet.create({
  digit: {
    width: BLOCK_W, 
    height: BLOCK_H, 
    flex: 1,
    tintColor: 'rgba(12,200,110,1)', 
    backgroundColor: 'rgba(128,19,210,1)', 
    resizeMode:'contain',
  },
  container: {
    width: null, 
    height: null, 
    tintColor: 'rgba(12,200,110,1)', 
    backgroundColor: 'rgba(128,19,210,1)', 
    borderBottomLeftRadius: 20, 
    borderTopLeftRadius: 20,
    resizeMode:'contain',
    position: "absolute",
  }
});

export default Digit 