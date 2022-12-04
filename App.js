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

export default App;
