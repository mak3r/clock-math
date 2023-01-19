import React from "react";
import { Text, Image, StyleSheet, View } from "react-native";


class Clock extends React.Component {
    constructor(props) {
      super(props);
      this.state = {date: new Date()};
    }
  
    componentDidMount() {
      this.timerID = setInterval(
        () => this.tick(),
        1000
      );
    }
  
    componentWillUnmount() {
      clearInterval(this.timerID);
    }
  
    tick() {
      this.setState({
        date: new Date()
      });
    }
  
    render() {
    const x = this.props.position[0];
    const y = this.props.position[1];
      return (
		  	<View style={[{ left: x, top: y }]}>
            <Text style={[{ left: x, top: y }]}>It is {this.state.date.toLocaleTimeString()}.</Text>
			{one}
			</View>
      );
    }

  }

  const styles = StyleSheet.create({
    digit: {
        width: 50, 
        height: 60, 
        flex: 1,
        tintColor: 'rgba(12,200,110,1)', 
        backgroundColor: 'rgba(128,19,210,1)', 
        resizeMode:'contain',
    }
    });

  const one = (
    <Image
      style={styles.digit}
      source={require('../assets/clock/numbers/one.png')}/>
    );

  const two = (
    <Image
      style={styles.digit}
      source={require('../assets/clock/numbers/two.png')}/>
    );



  export default Clock