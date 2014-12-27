jwtbrute
========

jwtbrute.exe takes a new line delimited list of canidate secrets in via stdin and tests them against a provided json web token to see if they were the secret used in the signing of the token. If the secret is discovered one can sign arbitry jwt tokens to bypass the authentication process of most webservices.

Example Usage
```batch
echo secret| jwtbrute.exe eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOjEyMzQ1Njc4OTAsIm5hbWUiOiJKb2huIERvZSIsImFkbWluIjp0cnVlfQ.eoaDVGTClRdfxUZXiPs3f8FmJDkDE_VCQFXqKxpLsts
```
Output
> Secret was: secret
