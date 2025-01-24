# PropertyListingAPI_Project

This project is a full-featured Property Listing and Booking API built with ASP.NET Core. It was designed to streamline room booking, cancellation, and information retrieval for properties near universities, making it a perfect demonstration of practical software engineering skills and cloud deployment practices.

# Overview

The Property Listing and Booking API provides a seamless way to manage room listings, book rooms, cancel bookings, calculate distances, and even retrieve weather information for property locations. The project also demonstrates the use of cloud hosting and integration with external services, making it an excellent example of a scalable and robust backend solution.

# Key Features
Room Listings: Provides an endpoint to retrieve detailed property information, including location, price, and distance from nearby universities.

Room Booking and Cancellation: Features intuitive endpoints to book or cancel rooms using room IDs.

Distance Calculation: Integrates with the Open Source Routing Machine (OSRM) API to calculate the distance between properties and universities based on their GPS coordinates.

Weather Integration: Fetches real-time weather data for property locations to enhance the user experience.

Cloud Deployment: The API is deployed on an Azure Linux Virtual Machine, demonstrating modern deployment practices.

# How It Works
# Development
The project was built using ASP.NET Core 6.0, leveraging C# for clean and efficient backend development.

RESTful API Design: The API follows standard RESTful principles, with clear endpoints for retrieving data, posting actions, and performing calculations.

JSON Serialization: Data is exchanged in JSON format, ensuring compatibility with modern client applications like web apps or mobile apps.

Struct and Serialization: A custom Room struct was used to model room data effectively, enabling serialization and deserialization of JSON responses.
Integration with External Services

# Distance Calculation:
Using the OSRM API, the service calculates distances between room locations and universities. This is particularly useful for users looking to evaluate travel times.

Example API call:

/api/Room/distance/{roomID}
Calculates and returns the distance between the specified room and the associated university.

# Weather Information:
Weather data is fetched from an external weather service, providing users with up-to-date environmental conditions at the room's location.

Example API call:

/api/Room/weather/{roomID}
Returns the current weather details for a specific room's location.
Deployment
The API was deployed on an Azure Linux Virtual Machine (VM). Key deployment details include:

Dotnet Runtime: The VM was set up with the necessary .NET runtime environment to host the application.

File Transfer: The project was transferred to the VM using SCP, ensuring all necessary files (including the publish folder and related assets like the CSV data file) were available for deployment.

Binding to Public IP: The API was configured to bind to 0.0.0.0 to make it accessible publicly via the Azure VM's static public IP address.
API Endpoints
Here’s a list of all the endpoints available in the API:

# Room Management
GET /api/Room
Retrieves a list of all available rooms, including details such as city, university, price, and distance.

GET /api/Room/bookedRooms
Retrieves a list of all rooms that have been booked.

GET /api/Room/canceledRooms
Retrieves a list of rooms whose bookings have been canceled.

# Booking and Cancellation
POST /api/Room/book/{roomID}
Books a room based on its unique room ID.

POST /api/Room/cancel/{roomID}
Cancels a booking for the specified room ID.

# Calculations and Information
GET /api/Room/distance/{roomID}
Calculates the distance between a room and the associated university using GPS coordinates.

GET /api/Room/weather/{roomID}
Fetches weather details for the room's location.

# Challenges and Solutions
Handling External Dependencies
OSRM and Weather APIs: The integration with OSRM for distance calculations and a weather API added real-world functionality but required careful handling of API responses and error handling to ensure reliability.
Deployment on Azure VM
Deploying on a Linux VM required configuring the server to expose the API publicly while ensuring it remained secure. The process involved:

# Setting up the .NET runtime.
Transferring files securely using SCP.
Binding the API to 0.0.0.0 for public access.
Testing endpoints thoroughly from both the VM and external clients.
Skills Demonstrated

API Design: Designed and implemented a clean, scalable RESTful API.

Cloud Deployment: Deployed and tested the application in a live production-like environment.

Integration: Worked with external APIs for enhanced functionality (e.g., OSRM and weather services).

Scalability Awareness: While the project is scalable by design, tools like Azure Monitor or JMeter can be used for load testing in the future.
Future Improvements

# Scalability Testing:
Scalability testing can be performed using tools like Azure Load Testing or JMeter to evaluate how the API handles high traffic.

# Additional Features:
Adding authentication to secure the endpoints.
Introducing filters for room listings (e.g., by price, distance, or city).

This project reflects a solid understanding of backend development, external API integration, and cloud deployment. It serves as a strong portfolio piece for showcasing skills in building and deploying real-world software solutions.
