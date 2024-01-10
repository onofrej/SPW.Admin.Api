// import http from 'k6/http';
// import { sleep, check } from 'k6';

// export let options = {
//     scenarios: {
//         constantLoad: {
//             // executor: 'constant-arrival-rate',
//             rate: 100, // Number of requests per second
//             timeUnit: '1s',
//             duration: '60s', // Duration of the test
//             preAllocatedVUs: 50, // Number of VUs (Virtual Users)
//             maxVUs: 100, // Maximum number of VUs
//         },
//     },
// };

// export default function () {
//     // Generate a random name
//     const randomName = generateRandomName();

//     // Prepare the request payload
//     const payload = JSON.stringify({
//         name: randomName,
//         email: 'gtest@test.com',
//         phoneNumber: '11111111111',
//         gender: 'string',
//         birthDate: '2023-12-29T15:36:15.980Z',
//         baptismDate: '2023-12-29T15:36:15.980Z',
//         privilege: 'string'
//     });

//     // Define the HTTP headers and options
//     const headers = { 'Content-Type': 'application/json' };
//     // const url = 'https://localhost:55539/users';
//     const url = 'https://yh5qgm6o37nkknhwhmrjm23u2q0sgytw.lambda-url.us-east-1.on.aws/users';

//     // Send POST request to the /users endpoint
//     const res = http.post(url, payload, { headers: headers });

//     // Check if the request was successful
//     check(res, {
//         'status is 200': (r) => r.status === 200,
//     });

//     // Add some sleep time between requests
//     sleep(1); // Adjust sleep time as needed
// }

// function generateRandomName() {
//     const names = ['Alice', 'Bob', 'Charlie', 'David', 'Emma', 'Frank', 'Grace', 'Henry', 'Ivy', 'Jack'];
//     return names[Math.floor(Math.random() * names.length)];
// }









// import http from 'k6/http';
// import { check } from 'k6';
// import { Rate } from 'k6/metrics';

// export const options = {
//   vus: 100,
//   duration: '1m',
//   rps: 500,
// };

// const requestBody = {
//   name: 'string',
//   email: 'gtest@test.com',
//   phoneNumber: '11111111111',
//   gender: 'string',
//   birthDate: '2023-12-29T15:36:15.980Z',
//   baptismDate: '2023-12-29T15:36:15.980Z',
//   privilege: 'string'
// };

// export default function () {
//   const url = 'https://localhost:55539/users';
//   const headers = {
//     'Content-Type': 'application/json',
//     'Authorization': 'Bearer <YOUR_TOKEN>', // Add authorization token if required
//     // Add other headers as needed
//   };

//   const params = {
//     headers: headers,
//   };

//   const response = http.post(url, JSON.stringify(requestBody), params);

//   // Check if the response status is 200 OK
//   check(response, {
//     'status is 200': (r) => r.status === 200,
//   });
// }










import http from 'k6/http';
import { check } from 'k6';
import { Rate } from 'k6/metrics';

export const options = {
  vus: 100,
  duration: '10s',
  rps: 500,
};

const requestBody = {
  name: generateRandomName(),
  email: 'gtest@test.com',
  phoneNumber: '11111111111',
  gender: 'string',
  birthDate: '2023-12-29T15:36:15.980Z',
  baptismDate: '2023-12-29T15:36:15.980Z',
  privilege: 'string'
};

export default function () {
  const url = 'https://localhost:55539/users';
//   const url = 'https://yh5qgm6o37nkknhwhmrjm23u2q0sgytw.lambda-url.us-east-1.on.aws/users';
  const params = {
    headers: {
      'Content-Type': 'application/json',
    },
  };

  const response = http.post(url, JSON.stringify(requestBody), params);

  // Check if the response status is 200 OK
  check(response, {
    'status is 200': (r) => r.status === 200,
  });
}


function generateRandomName() {
    const names = ['Alice', 'Bob', 'Charlie', 'David', 'Emma', 'Frank', 'Grace', 'Henry', 'Ivy', 'Jack'];
    return names[Math.floor(Math.random() * names.length)];
}

