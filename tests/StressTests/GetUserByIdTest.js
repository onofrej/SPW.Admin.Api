import http from 'k6/http';
import { check } from 'k6';

export const options = {
  vus: 100,
  duration: '30s', // Duration of the test
  rps: 100,
};

export default function () {
  const url = 'https://localhost:55539/users/b3068844-6b0b-49d6-bca8-f37c7e83265b';
  const headers = {
    // Add headers if required
    'Content-Type': 'application/json',
    // 'Authorization': 'Bearer <YOUR_TOKEN>', // Include authorization token if needed
  };

  const params = {
    headers: headers,
  };

  const response = http.get(url, params);

  // Check if the response status is 200 OK
  check(response, {
    'status is 200': (r) => r.status === 200,
  });
}
