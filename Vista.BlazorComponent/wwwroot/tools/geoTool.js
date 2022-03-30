/* HTML Geolocation API */
/* ref→https://www.w3schools.com/html/html5_geolocation.asp */

export function getLocation() {
  return Promise.resolve(new Promise((resolve, reject) => {
    function successCallback(position) {
      //console.log('getCurrentPosition SUCCESS', { position });
      resolve({
        coords: {
          latitude: position.coords.latitude,
          longitude: position.coords.longitude,
          altitude: position.coords.altitude,
          accuracy: position.coords.accuracy,
          altitudeAccuracy: position.coords.altitudeAccuracy,
          heading: position.coords.heading,
          speed: position.coords.speed
        },
        timestamp: position.timestamp
      });
    }

    function errorCallback(error) {
      switch (error.code) {
        case error.PERMISSION_DENIED:
          reject('User denied the request for Geolocation.');
          break;
        case error.POSITION_UNAVAILABLE:
          reject('Location information is unavailable.');
          break;
        case error.TIMEOUT:
          reject('The request to get user location timed out.');
          break;
        case error.UNKNOWN_ERROR:
          reject('An unknown error occurred.');
          break;
        default:
          reject(error);
          break;
      }
    }

    // GO
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(successCallback, errorCallback);
    } else {
      reject('Geolocation is not supported by this browser.');
    }
  }));
}

export function watchPosition(dotNetObject)
{
  function successCallback(position) {
    console.log('watchPosition:yield =>', { position });
    const responsePosition = {
      coords: {
        latitude: position.coords.latitude,
        longitude: position.coords.longitude,
        altitude: position.coords.altitude,
        accuracy: position.coords.accuracy,
        altitudeAccuracy: position.coords.altitudeAccuracy,
        heading: position.coords.heading,
        speed: position.coords.speed
      },
      timestamp: position.timestamp
    };

    dotNetObject.invokeMethodAsync('OnWatchResponse', responsePosition);
  }

  function errorCallback(error) {
    switch (error.code) {
      case error.PERMISSION_DENIED:
        throw 'User denied the request for Geolocation.';
      case error.POSITION_UNAVAILABLE:
        throw 'Location information is unavailable.';
      case error.TIMEOUT:
        throw 'The request to get user location timed out.';
      case error.UNKNOWN_ERROR:
        throw 'An unknown error occurred.';
      default:
        throw error;
    }
  }

  if (navigator.geolocation) {
    const watchId = navigator.geolocation.watchPosition(successCallback, errorCallback);
    return watchId;
  } else {
    throw 'Geolocation is not supported by this browser.';
  }
}