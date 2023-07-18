import { vi } from "vitest";
import { Planet } from "./individualPlanetPageDataLoader";
import { IndividualPlanetPage } from "..";
import { render, screen } from "@testing-library/react";

const planet: Planet = {
  id: "earth",
  name: "Earth",
  imageUrl: "https://space-images.com/earth",
  diameterDisplayValue: "12,345 km",
  distanceToSunDisplayValue: "50.83 million km",
  massDisplayValue: "1.123 x 10^21 kg",
  additionalInfo: "This planet has one moon 🧀",
};

describe("IndividualPlanetPage", () => {
  vi.mock("react-router-dom", () => ({
    useLoaderData: vi.fn(() => ({
      planet,
    })),
  }));

  beforeEach(() => {
    render(<IndividualPlanetPage />);
  });

  it("should render the planet's name", () => {
    expect(
      screen.getByRole("heading", { name: planet.name })
    ).toBeInTheDocument();
  });

  it("should render the planet's image", () => {
    const image = screen.getByRole("img");

    expect(image).toBeInTheDocument();
    expect(image).toHaveAttribute("src", planet.imageUrl);
  });

  it("should render the planet's diameter", () => {
    expect(
      screen.getByText(`Diameter: ${planet.diameterDisplayValue}`)
    ).toBeInTheDocument();
  });

  it("should render the planet's distance to the sun", () => {
    expect(
      screen.getByText(
        `Distance to the sun: ${planet.distanceToSunDisplayValue}`
      )
    ).toBeInTheDocument();
  });

  it("should render the planet's mass", () => {
    expect(
      screen.getByText(`Mass: ${planet.massDisplayValue}`)
    ).toBeInTheDocument();
  });

  it("should render the planet's additional info", () => {
    expect(screen.getByText(planet.additionalInfo!)).toBeInTheDocument();
  });
});
