import http from 'k6/http';
import { sleep, check } from 'k6';

export let options = {
    scenarios: {
        constantLoad: {
            executor: 'constant-arrival-rate',
            rate: 5000, // Number of requests per second
            timeUnit: '1s',
            duration: '360s', // Duration of the test
            preAllocatedVUs: 50, // Number of VUs (Virtual Users)
            maxVUs: 100, // Maximum number of VUs
        },
    },
};

export default function () {
    // Generate a random name
    const randomName = generateRandomName();

    // Prepare the request payload
    const payload = JSON.stringify({
        name: randomName,
    });

    // Define the HTTP headers and options
    const headers = { 'Content-Type': 'application/json' };
    const url = 'https://localhost:7034/users';
    // const url = 'https://yh5qgm6o37nkknhwhmrjm23u2q0sgytw.lambda-url.us-east-1.on.aws/users';

    // Send POST request to the /users endpoint
    const res = http.post(url, payload, { headers: headers });

    // Check if the request was successful
    check(res, {
        'status is 200': (r) => r.status === 200,
    });

    // Add some sleep time between requests
    sleep(1); // Adjust sleep time as needed
}

function generateRandomName() {
    const names = ['Alice', 'Bob', 'Charlie', 'David', 'Emma', 'Frank', 'Grace', 'Henry', 'Ivy', 'Jack'];
    return names[Math.floor(Math.random() * names.length)];
}
