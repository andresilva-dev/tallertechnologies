# Taller technologies
Products project

## Authentication

JWT authentication has been implemented with User Role submitted via Bearer Token.

### Testing

To test the application, it's necessary to register a user first. At this moment, the token will be returned. If needed, the token can also be obtained via login.

All endpoints can only be consumed using the token.

### Soft Delete Implementation

As agreed during the interview, soft delete has been implemented using the `IsActive` property. If a product is deleted via soft-delete, it should no longer be displayed in listings or considered in other endpoints.
