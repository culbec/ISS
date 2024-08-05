package com.iss.Domain;

import java.util.*;

/**
 *
 */
class User extends Entity<Integer> {
    private final String name;
    private final String cnp;
    private final String username;
    private final Role role;

    /**
     * Default constructor
     */
    User(Integer id, String name, String cnp, String username, Role role) {
        super(id);
        this.name = name;
        this.cnp = cnp;
        this.username = username;
        this.role = role;
    }

    public String getName() {
        return name;
    }

    public String getCnp() {
        return cnp;
    }

    public String getUsername() {
        return username;
    }

    public Role getRole() {
        return role;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        User user = (User) o;
        return Objects.equals(name, user.name) && Objects.equals(cnp, user.cnp) && Objects.equals(username, user.username) && role == user.role;
    }

    @Override
    public int hashCode() {
        return Objects.hash(super.hashCode(), name, cnp, username, role);
    }

    @Override
    public String toString() {
        return "User{" +
                "name='" + name + '\'' +
                ", cnp='" + cnp + '\'' +
                ", username='" + username + '\'' +
                ", role=" + role +
                "} " + super.toString();
    }
}