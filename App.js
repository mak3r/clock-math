<<<<<<< Updated upstream
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Text, View, Image } from 'react-native';
import React from 'react'

const App = () => {
  return (
    <View style={styles.container}>
      <Text>Open up App.js to start working on your app!</Text>
	  <Image
	  	source={require('./assets/clock/numbers/one.png')}
		style={{width:100, height:100, tintColor: 'rgba(12,200,110,1)', backgroundColor: 'rgba(128,19,210,1)', borderBottomLeftRadius: 20, borderTopLeftRadius: 20,resizeMode:'contain'}}
		/>
	  <Image
	  	source={require('./assets/clock/numbers/two.png')}
		style={{width:100, height:100, tintColor: 'rgba(12,200,110,1)', backgroundColor: 'rgba(128,19,210,1)', borderBottomLeftRadius: 20, borderTopLeftRadius: 20,resizeMode:'contain'}}
		/>
      <StatusBar style="auto" />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'rgba(128,19,210,0)',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
=======
import React, { useRef, useState } from 'react';
import { View, Image, PanResponder, Animated } from 'react-native';

const App = () => {
  const pan1 = useRef(new Animated.ValueXY()).current;
  const pan2 = useRef(new Animated.ValueXY()).current;
  const [isColliding, setIsColliding] = useState(false);

  const panResponder1 = useRef(
    PanResponder.create({
      onMoveShouldSetPanResponder: () => true,
      onPanResponderMove: Animated.event([null, { dx: pan1.x, dy: pan1.y }]),
      onPanResponderRelease: (e, gestureState) => {
        if (isColliding) {
          Animated.spring(pan2, { toValue: { x: -200, y: 0 }, useNativeDriver: true }).start();
        }
      },
    })
  ).current;

  const panResponder2 = useRef(
    PanResponder.create({
      onMoveShouldSetPanResponder: () => true,
      onPanResponderMove: Animated.event([null, { dx: pan2.x, dy: pan2.y }]),
    })
  ).current;

  const checkCollision = () => {
    const image1Position = {
      x: pan1.x._value + 50, // assuming image1 width is 100
      y: pan1.y._value + 50, // assuming image1 height is 100
    };

    const image2Position = {
      x: pan2.x._value + 50, // assuming image2 width is 100
      y: pan2.y._value + 50, // assuming image2 height is 100
    };

    const collision =
      image1Position.x >= image2Position.x &&
      image1Position.x <= image2Position.x + 100 &&
      image1Position.y >= image2Position.y &&
      image1Position.y <= image2Position.y + 100;

    setIsColliding(collision);
  };

  return (
    <View>
      <Animated.Image
        source={require('./assets/clock/numbers/one.png')}
        {...panResponder1.panHandlers}
        style={{ transform: [{ translateX: pan1.x }, { translateY: pan1.y }] }}
        onLayout={checkCollision}
      />
      <Animated.Image
        source={require('./assets/clock/numbers/two.png')}
        {...panResponder2.panHandlers}
        style={{ transform: [{ translateX: pan2.x }, { translateY: pan2.y }] }}
        onLayout={checkCollision}
      />
    </View>
  );
};
>>>>>>> Stashed changes

export default App;
