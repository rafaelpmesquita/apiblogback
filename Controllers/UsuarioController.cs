// Intentionally contains no controller or public endpoints.
// The previous /v1/user endpoint exposed user records, including the stored
// password-derived value, and allowed unauthenticated writes. Reintroduce user
// management only after implementing and testing secure authentication,
// authorization, and password hashing with a dedicated password-hashing API.
