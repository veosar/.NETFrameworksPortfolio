
# S3

This project showcases simple S3 usage along with Lambda Function for performing simple operations in reaction to file being added to S3.

In order to use this project you will need to authenticate your machine against AWS in scope for S3 + Lambda Functions for S3.

You can use docker compose to run this API. To do this you will need to configure AWS_CREDENTIALS_LOCATION environment variable.

## S3Api

This is a simple Api that allows user to add, update, get or delete an image into S3 bucket.

| Command | Endpoint | Description
|--|--|--|
| GET | http://localhost:8080/images/{id} | Gets image with key {id} |
| GET | http://localhost:8080/images-blurred/{id} | Gets blurred version of image with key {id} |
| POST | http://localhost:8080/images | Creates or updates image under key {id} |
| DELETE | http://localhost:8080/images/{id}| Deletes an image under key {id} |

## S3ImageHandler

This is a simple lambda S3 function that reacts to image being added and creates a second, blurred copy of the image under different key prefix.
It also reacts to deletion of original file and deletes the blurred version if it exists.