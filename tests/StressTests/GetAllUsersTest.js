import http from 'k6/http';
import { sleep, check } from 'k6';

export let options = {
  scenarios: {
      constantLoad: {
          executor: 'constant-arrival-rate',
          rate: 10, // Number of requests per second
          timeUnit: '1s',
          duration: '30s', // Duration of the test
          preAllocatedVUs: 50, // Number of VUs (Virtual Users)
          maxVUs: 100, // Maximum number of VUs
      },
  },
};

export default function () {
    
    // Define the HTTP headers and options
    const headers = { 'Access-Control-Allow-Origin': 'Access-Control-Allow-Methods' };
    const url = 'https://yh5qgm6o37nkknhwhmrjm23u2q0sgytw.lambda-url.us-east-1.on.aws/users';
    // const url = 'https://localhost:55539/users';

    // Send POST request to the /users endpoint
    const res = http.get(url, { headers: headers });

    // Check if the request was successful
    check(res, {
        'status is 200': (r) => r.status === 200,
    });

    // Add some sleep time between requests
    sleep(1); // Adjust sleep time as needed
}