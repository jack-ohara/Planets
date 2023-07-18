import { describe, it, vi } from "vitest";
import { render, screen } from "@testing-library/react";
import { HomePage } from "..";
import { PropsWithChildren } from "react";

const planets = [
  {
    id: "mars",
    name: "Mars",
  },
  {
    id: "earth",
    name: "Earth",
  },
  {
    id: "jupiter",
    name: "Jupiter",
  },
];

describe("HomePage", () => {
  vi.mock("react-router-dom", () => ({
    useLoaderData: vi.fn(() => ({
      planets: planets,
    })),
    Link: ({ to, children }: PropsWithChildren<{ to: string }>) => (
      <a href={to}>{children}</a>
    ),
  }));

  it("should render the list of planets with the correct links", () => {
    render(<HomePage />);

    planets.forEach((planet) => {
      const link = screen.getByRole("link", { name: planet.name });
      expect(link).toBeInTheDocument();
      expect(link).toHaveAttribute("href", `/${planet.id}`);
    });
  });
});
