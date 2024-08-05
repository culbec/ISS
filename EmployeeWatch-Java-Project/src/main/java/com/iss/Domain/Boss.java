package com.iss.Domain;

import java.util.Objects;

public class Boss extends User {
    private final Position position;

    public Boss(Integer id, String name, String cnp, String username, Role role, Position position) {
        super(id, name, cnp, username, role);
        this.position = position;
    }

    public Position getPosition() {
        return position;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        Boss boss = (Boss) o;
        return position == boss.position;
    }

    @Override
    public int hashCode() {
        return Objects.hash(super.hashCode(), position);
    }

    @Override
    public String toString() {
        return "Boss{" +
                "position=" + position +
                "} " + super.toString();
    }
}